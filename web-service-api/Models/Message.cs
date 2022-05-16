namespace web_service_api
{
public class Message
{
    public int id = default(int);   
    
    public DateTime created { get; set; }
    
    public string sender { get; set; }

    public string reciver { get; set; }
    
    public string content { get; set; }
    
}
}

