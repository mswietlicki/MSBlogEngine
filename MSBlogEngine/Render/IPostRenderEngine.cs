namespace MSBlogEngine.Render
{
    public interface IPostRenderEngine
    {
        string Render(Models.BlogPost post);
    }
}