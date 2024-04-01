namespace JsonObject;

public class Rootobject
{
    public string _object { get; set; }
    public Result[] results { get; set; }
    public object next_cursor { get; set; }
    public bool has_more { get; set; }
    public string type { get; set; }
    public Page_Or_Database page_or_database { get; set; }
    public string request_id { get; set; }

    public override string ToString()
    {
        string rtn = string.Empty;
        foreach (var item in results)
        {
            rtn += $"[id] : {item.id}, [소속] : {item.properties.소속.select?.name}, [이름] : {item.properties.이름.title[0]?.plain_text}{Environment.NewLine}";
        }
        return rtn;
    }
}

public class QueryOption
{
    public int page_size { get; set; }
    public string start_cursor { get; set; }
}

public class QueryFilter
{
    public QueryProperty filter { get; set; }
}

public class QueryProperty
{
    public string property { get; set; }
    public QuerySelect select { get; set; }
}

public class QuerySelect
{
    public string equals { get; set; }
}

public class QueryAdd
{
    public QueryParent parent { get; set; }
    public QueryProperties properties { get; set; }
}

public class QueryParent
{
    public string database_id { get; set; }
}

public class QueryProperties
{
    public Query소속 소속 { get; set; }
    public Query이름 이름 { get; set; }
}

public class Query소속
{
    public 소속Select select { get; set; }
}

public class 소속Select
{
    public string name { get; set; }
}

public class Query이름
{
    public 이름Title[] title { get; set; }
}

public class 이름Title
{
    public 이름Text text { get; set; }
}

public class 이름Text
{
    public string content { get; set; }
}

public class Page_Or_Database
{
}

public class Result
{
    public string _object { get; set; }
    public string id { get; set; }
    public DateTime created_time { get; set; }
    public DateTime last_edited_time { get; set; }
    public Created_By created_by { get; set; }
    public Last_Edited_By last_edited_by { get; set; }
    public object cover { get; set; }
    public object icon { get; set; }
    public Parent parent { get; set; }
    public bool archived { get; set; }
    public Properties properties { get; set; }
    public string url { get; set; }
    public object public_url { get; set; }
}

public class Created_By
{
    public string _object { get; set; }
    public string id { get; set; }
}

public class Last_Edited_By
{
    public string _object { get; set; }
    public string id { get; set; }
}

public class Parent
{
    public string type { get; set; }
    public string database_id { get; set; }
}

public class Properties
{
    public 소속 소속 { get; set; }
    public 이름 이름 { get; set; }
}

public class 소속
{
    public string id { get; set; }
    public string type { get; set; }
    public Select select { get; set; }
}

public class Select
{
    public string id { get; set; }
    public string name { get; set; }
    public string color { get; set; }
}

public class 이름
{
    public string id { get; set; }
    public string type { get; set; }
    public Title[] title { get; set; }
}

public class Title
{
    public string type { get; set; }
    public Text text { get; set; }
    public Annotations annotations { get; set; }
    public string plain_text { get; set; }
    public object href { get; set; }
}

public class Text
{
    public string content { get; set; }
    public object link { get; set; }
}

public class Annotations
{
    public bool bold { get; set; }
    public bool italic { get; set; }
    public bool strikethrough { get; set; }
    public bool underline { get; set; }
    public bool code { get; set; }
    public string color { get; set; }
}