namespace ETL_web_project.DTOs.Etl.Staging
{
    public class StagingQualityDto
    {
        public int MissingStoreCodeCount { get; set; }
        public int MissingProductCodeCount { get; set; }
        public int InvalidQuantityCount { get; set; }
        public int InvalidPriceCount { get; set; }
    }
}
