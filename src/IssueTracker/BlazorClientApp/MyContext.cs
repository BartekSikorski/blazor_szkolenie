namespace BlazorClientApp;

public class MyContext 
{
    public int Counter { get; set; }

    protected MyContext()
    {
        
    }

    private static MyContext _instance;
    public static MyContext Instance
    {
        get
        {
            if (_instance == null )
                _instance = new MyContext();

            return _instance;
        }
    }
}
