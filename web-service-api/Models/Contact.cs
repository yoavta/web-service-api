namespace web_service_api
{
public class Contact
{
    public string myContact { get; set; }

    public string id { get; set; }

    public string name { get; set; }

    public string? last { get; set; }

    public DateTime? lastdate { get; set; }
    
    public string server { get; set; }


    public int key = default(int);
}
}

