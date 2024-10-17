namespace KoiDeliveryOrdering.MVCWebApp.Payloads.Responses
{
    public class LocationResponse
    {
        public int PlaceId { get; set; }
        public string Licence { get; set; } = string.Empty;
        public string OsmType { get; set; } = string.Empty;
        public long OsmId { get; set; }
        public string Lat { get; set; } = string.Empty; 
        public string Lon { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int PlaceRank { get; set; }
        public double Importance { get; set; }
        public string Addresstype { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string[] BoundingBox { get; set; } = null!;
    }
}
