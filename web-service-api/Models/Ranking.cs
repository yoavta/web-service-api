namespace web_service_api
{
    public class Ranking
    {
        public int id = default(int);

        public DateTime created { get; set; }

        public string name { get; set; }

        public string text { get; set; }

        public int rank { get; set; }
    }
}
