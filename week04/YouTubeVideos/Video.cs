using System.Collections.Generic;

public class Video
{
    public string _title;
    public string _author;
    public int _lengthInSeconds;
    private List<Comment> _comments = new List<Comment>();

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }
}