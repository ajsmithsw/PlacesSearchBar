using System;
using System.Collections.Generic;
using System.Linq;

namespace DurianCode.PlacesSearchBar
{
	public enum PlacesField
	{
		// [Basic] fields

		AddressComponents,
		Address,
		AlternativeID,
		FormattedAddress,
		Location,
		Viewport,
		Icon,
		ID,
		Name,
		PermanentlyClosed,
		Photo,
		PlaceID,
		PlusCode,
		Scope,
		Type,
		URL,
		UTCOffset,
		Vicinity,
		
		// [Contact] fields

		PhoneNumber,
		InternationalPhoneNumber,
		OpeningHours,
		Website,
		
		// [Atmosphere] fields

		PriceLevel,
		Rating,
		Reviews,
		UserRatingsTotal,

	}

	public class PlacesFieldList
	{
		public static readonly PlacesFieldList ALL     = new PlacesFieldList();

		public static readonly PlacesFieldList DEFAULT = new PlacesFieldList(
			PlacesField.AddressComponents, 
			PlacesField.FormattedAddress, 
			PlacesField.Location,
			PlacesField.ID,
			PlacesField.Name,
			PlacesField.PlaceID);

		public readonly IReadOnlyList<PlacesField> Fields;
		
		public PlacesFieldList(params PlacesField[] fields)
		{
			Fields = fields.ToList().AsReadOnly();
		}

		public string ToString(PlacesField field)
		{
			switch (field)
			{
				case PlacesField.AddressComponents:        return "address_component";
				case PlacesField.Address:                  return "adr_address";
				case PlacesField.AlternativeID:            return "alt_id";
				case PlacesField.FormattedAddress:         return "formatted_address";
				case PlacesField.Location:                 return "geometry/location";
				case PlacesField.Viewport:                 return "geometry/viewport";
				case PlacesField.Icon:                     return "icon";
				case PlacesField.ID:                       return "id";
				case PlacesField.Name:                     return "name";
				case PlacesField.PermanentlyClosed:        return "permanently_closed";
				case PlacesField.Photo:                    return "photo";
				case PlacesField.PlaceID:                  return "place_id";
				case PlacesField.PlusCode:                 return "plus_code";
				case PlacesField.Scope:                    return "scope";
				case PlacesField.Type:                     return "type";
				case PlacesField.URL:                      return "url";
				case PlacesField.UTCOffset:                return "utc_offset";
				case PlacesField.Vicinity:                 return "vicinity";
				case PlacesField.PhoneNumber:              return "formatted_phone_number";
				case PlacesField.InternationalPhoneNumber: return "international_phone_number";
				case PlacesField.OpeningHours:             return "opening_hours";
				case PlacesField.Website:                  return "website";
				case PlacesField.PriceLevel:               return "price_level";
				case PlacesField.Rating:                   return "rating";
				case PlacesField.Reviews:                  return "review";
				case PlacesField.UserRatingsTotal:         return "user_ratings_total";
			}

			throw new ArgumentException("Invalid PlacesField");
		}

		public bool IsEmpty()
		{
			return Fields.Any();
		}

		public override string ToString()
		{
			return string.Join(",", Fields.Select(ToString));
		}
	}
}
