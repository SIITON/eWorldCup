namespace eWorldCup.Core.RailwayOriented;

public class Example
{
    public Result<ReturnObjekt> DoAllTheStuff()
    {
        return GetSomething()
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething)
            .OnSuccess(ChangeSomething);
    }
    
    public Result<ReturnObjekt> GetSomething()
    {
        var o = new ReturnObjekt("hej");
        
        return Result<ReturnObjekt>.Success(o);
    }
    
    public Result<ReturnObjekt> ChangeSomething(ReturnObjekt anObject)
    {
        anObject.Count++;

        return Result<ReturnObjekt>.Success(anObject);
    }
    
}

public record ReturnObjekt(string Name)
{
    public int Count { get; set; }
}
