using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Asset usage example.
	/// </summary>
	public class LocalizationSource : MonoBehaviour
	{

		string archiveName = "LanguageData";

		LanguageData saveData;

		/// <summary>
		/// Called on app start.
		/// </summary>
		public void Awake()
		{
			LocalizationManager.Read();

			saveData = new LanguageData();
			if (BinarySerialization.IsFileExist(archiveName))
			{
				saveData = BinarySerialization.Deserialize<LanguageData>(archiveName);
				LocalizationManager.Language = saveData.language;
			}
			else
			{
				switch (Application.systemLanguage)
				{
					case SystemLanguage.Spanish:
						LocalizationManager.Language = "Spanish";
						break;
					case SystemLanguage.English:
						LocalizationManager.Language = "English";
						break;
					default:
						LocalizationManager.Language = "English";
						break;
				}

				saveData.language = LocalizationManager.Language;
				BinarySerialization.Serialize(archiveName, saveData);
			}
		}

	/// <summary>
	/// Change localization at runtime
	/// </summary>
	public void SetLocalization(string localization)
		{
			LocalizationManager.Language = localization;
			saveData.language = LocalizationManager.Language;
			BinarySerialization.Serialize(archiveName, saveData);
		}
	}
}