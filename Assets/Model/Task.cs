using System.Collections;
using System.Collections.Generic;

public class Task {
    public Task(string id, string title, bool isCompleted) {
        Id = id;
        Title = title;
        IsCompleted = isCompleted;
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}
