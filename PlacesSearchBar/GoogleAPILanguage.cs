namespace DurianCode.PlacesSearchBar
{
	public enum GoogleAPILanguage
	{
		Unset,

		Afrikaans,
		Albanian,
		Amharic,
		Arabic,
		Armenian,
		Azerbaijani,
		Basque,
		Belarusian,
		Bengali,
		Bosnian,
		Bulgarian,
		Burmese,
		Catalan,
		Chinese,
		ChineseSimplified,
		ChineseHongKong,
		ChineseTraditional,
		Croatian,
		Czech,
		Danish,
		Dutch,
		English,
		EnglishAustralian,
		EnglishGreatBritain,
		Estonian,
		Farsi,
		Finnish,
		Filipino,
		French,
		FrenchCanada,
		Galician,
		Georgian,
		German,
		Greek,
		Gujarati,
		Hebrew,
		Hindi,
		Hungarian,
		Icelandic,
		Indonesian,
		Italian,
		Japanese,
		Kannada,
		Kazakh,
		Khmer,
		Korean,
		Kyrgyz,
		Lao,
		Latvian,
		Lithuanian,
		Macedonian,
		Malay,
		Malayalam,
		Marathi,
		Mongolian,
		Nepali,
		Norwegian,
		Polish,
		Portuguese,
		PortugueseBrazil,
		PortuguesePortugal,
		Punjabi,
		Romanian,
		Russian,
		Serbian,
		Sinhalese,
		Slovak,
		Slovenian,
		Spanish,
		SpanishLatinAmerica,
		Swahili,
		Swedish,
		Tamil,
		Telugu,
		Thai,
		Turkish,
		Ukrainian,
		Urdu,
		Uzbek,
		Vietnamese,
		Zulu,
	}

	class GoogleAPILanguageHelper
	{
		public static string ToAPIString(GoogleAPILanguage lng)
		{
			// https://developers.google.com/maps/faq#languagesupport

			switch (lng)
			{
				case GoogleAPILanguage.Afrikaans:               return "af";
				case GoogleAPILanguage.Albanian:                return "sq";
				case GoogleAPILanguage.Amharic:                 return "am";
				case GoogleAPILanguage.Arabic:                  return "ar";
				case GoogleAPILanguage.Armenian:                return "ar";
				case GoogleAPILanguage.Azerbaijani:             return "az";
				case GoogleAPILanguage.Basque:                  return "eu";
				case GoogleAPILanguage.Belarusian:              return "be";
				case GoogleAPILanguage.Bengali:                 return "bn";
				case GoogleAPILanguage.Bosnian:                 return "bs";
				case GoogleAPILanguage.Bulgarian:               return "bg";
				case GoogleAPILanguage.Burmese:                 return "my";
				case GoogleAPILanguage.Catalan:                 return "ca";
				case GoogleAPILanguage.Chinese:                 return "zh";
				case GoogleAPILanguage.ChineseSimplified:       return "zh-CN";
				case GoogleAPILanguage.ChineseHongKong:         return "zh-HK";
				case GoogleAPILanguage.ChineseTraditional:      return "zh-TW";
				case GoogleAPILanguage.Croatian:                return "hr";
				case GoogleAPILanguage.Czech:                   return "cs";
				case GoogleAPILanguage.Danish:                  return "da";
				case GoogleAPILanguage.Dutch:                   return "nl";
				case GoogleAPILanguage.English:                 return "en";
				case GoogleAPILanguage.EnglishAustralian:       return "en-AU";
				case GoogleAPILanguage.EnglishGreatBritain:     return "en-GB";
				case GoogleAPILanguage.Estonian:                return "et";
				case GoogleAPILanguage.Farsi:                   return "fa";
				case GoogleAPILanguage.Finnish:                 return "fi";
				case GoogleAPILanguage.Filipino:                return "fil";
				case GoogleAPILanguage.French:                  return "fr";
				case GoogleAPILanguage.FrenchCanada:            return "fr-CA";
				case GoogleAPILanguage.Galician:                return "gl";
				case GoogleAPILanguage.Georgian:                return "ka";
				case GoogleAPILanguage.German:                  return "de";
				case GoogleAPILanguage.Greek:                   return "el";
				case GoogleAPILanguage.Gujarati:                return "gu";
				case GoogleAPILanguage.Hebrew:                  return "iw";
				case GoogleAPILanguage.Hindi:                   return "hi";
				case GoogleAPILanguage.Hungarian:               return "hu";
				case GoogleAPILanguage.Icelandic:               return "is";
				case GoogleAPILanguage.Indonesian:              return "id";
				case GoogleAPILanguage.Italian:                 return "it";
				case GoogleAPILanguage.Japanese:                return "ja";
				case GoogleAPILanguage.Kannada:                 return "kn";
				case GoogleAPILanguage.Kazakh:                  return "kk";
				case GoogleAPILanguage.Khmer:                   return "km";
				case GoogleAPILanguage.Korean:                  return "ko";
				case GoogleAPILanguage.Kyrgyz:                  return "ky";
				case GoogleAPILanguage.Lao:                     return "lo";
				case GoogleAPILanguage.Latvian:                 return "lv";
				case GoogleAPILanguage.Lithuanian:              return "lt";
				case GoogleAPILanguage.Macedonian:              return "mk";
				case GoogleAPILanguage.Malay:                   return "ms";
				case GoogleAPILanguage.Malayalam:               return "ml";
				case GoogleAPILanguage.Marathi:                 return "mr";
				case GoogleAPILanguage.Mongolian:               return "mn";
				case GoogleAPILanguage.Nepali:                  return "ne";
				case GoogleAPILanguage.Norwegian:               return "no";
				case GoogleAPILanguage.Polish:                  return "pl";
				case GoogleAPILanguage.Portuguese:              return "pt";
				case GoogleAPILanguage.PortugueseBrazil:        return "pt-BR";
				case GoogleAPILanguage.PortuguesePortugal:      return "pt-PT";
				case GoogleAPILanguage.Punjabi:                 return "pa";
				case GoogleAPILanguage.Romanian:                return "ro";
				case GoogleAPILanguage.Russian:                 return "ru";
				case GoogleAPILanguage.Serbian:                 return "sr";
				case GoogleAPILanguage.Sinhalese:               return "si";
				case GoogleAPILanguage.Slovak:                  return "sk";
				case GoogleAPILanguage.Slovenian:               return "sl";
				case GoogleAPILanguage.Spanish:                 return "es";
				case GoogleAPILanguage.SpanishLatinAmerica:     return "es-419";
				case GoogleAPILanguage.Swahili:                 return "sw";
				case GoogleAPILanguage.Swedish:                 return "sv";
				case GoogleAPILanguage.Tamil:                   return "ta";
				case GoogleAPILanguage.Telugu:                  return "te";
				case GoogleAPILanguage.Thai:                    return "th";
				case GoogleAPILanguage.Turkish:                 return "tr";
				case GoogleAPILanguage.Ukrainian:               return "uk";
				case GoogleAPILanguage.Urdu:                    return "ur";
				case GoogleAPILanguage.Uzbek:                   return "uz";
				case GoogleAPILanguage.Vietnamese:              return "vi";
				case GoogleAPILanguage.Zulu:                    return "zu";
					
				case GoogleAPILanguage.Unset                    return "zu";
				default:                                        return "";
			}
		}
	}
}
