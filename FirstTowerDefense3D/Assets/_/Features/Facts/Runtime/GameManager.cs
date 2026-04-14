using System;
using UnityEngine;

namespace TheFundation.Runtime
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager m_Instance { get; private set; }
        public static FactDictionary Facts { get; } = new();
        public const int _MaxSlots = 10;
        public VersionDefinition m_versionDefinition;
        public static bool IsGameLoaded { get; private set; }

        void Awake()
        {
            if (m_Instance) { Destroy(gameObject); return; }
            m_Instance = this;
            DontDestroyOnLoad(gameObject);

            // 1️⃣ Load FIRST
            FactSaveSystem.LoadFromFile(Facts);
            
            DeclareGameFacts();
            IsGameLoaded = true;

            // 2️⃣ Language (now safe)
            if (Facts.TryGetFact("language", out string lang))
                LocalizationManager.SetLanguage(lang);
            else 
                LocalizationManager.SetLanguage("en");

            // 3️⃣ Platform
            PlatformizerService.Initialize();

            // 4️⃣ Settings
            SettingsService.Initialize(_settingsDefinitions);

            // 5️⃣ Goals must be initialized AFTER Load
            GoalsService.Initialize(_Goals);

            // 6️⃣ Version
            VersionService.m_definition = m_versionDefinition;
            VersionService.Initialize();

            // 7️⃣ Has save
            RefreshHasSaveFact();

        }

        private void Update()
        {
            _saveTimer += Time.deltaTime;

            if (_saveTimer >= _autoSaveInterval)
            {
                _saveTimer = 0;
                AutoSave();
            }
        }

        void OnApplicationPause(bool p)
        {
            if (p) FactSaveSystem.SaveToFile(Facts);
        }
        void OnApplicationQuit() => FactSaveSystem.SaveToFile(Facts);

        // API
        public static void SaveToSlot(int slot) => FactSaveSystem.SaveToSlot(Facts, slot);
        public static void LoadFromSlot(int slot) => FactSaveSystem.LoadFromSlot(Facts, slot);
        public static void DeleteSlot(int slot) => FactSaveSystem.DeleteSlot(slot);
        public static bool HasSaveInSlot(int slot) => FactSaveSystem.SlotExist(slot);

        public static bool AnySaveExists()
        {
            for (int i = 0; i < _MaxSlots; i++)
                if (HasSaveInSlot(i))  return true;
            return false;
        }

        public void RefreshHasSaveFact()
        {
            bool any = AnySaveExists();
            Facts.SetFact("has_save", any, FactDictionary.FactPersistence.Normal);
        }

        private void DeclareGameFacts()
        {
            foreach (var mono in FindObjectsOfType<MonoBehaviour>(true))
            {
                if (mono is IGameFactsProvider provider)
                {
                    provider.DeclareFacts(Facts);
                }
            }
        }

        private void AutoSave()
        {
            FactSaveSystem.SaveToFile(Facts);
        }

        [SerializeField] private SettingsDefinitionCollection _settingsDefinitions;
        [SerializeField] private GoalsCollection _Goals;
        [SerializeField] private float _autoSaveInterval = 5f;
        private float _saveTimer = 0f;
    }
}