using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace DayCare.ViewModels;

public partial class FeedViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _newPostContent = string.Empty;

    public ObservableCollection<FeedPost> Posts { get; } = new();

    public FeedViewModel()
    {
        Title = "Feed";
        LoadSamplePosts();
    }

    private void LoadSamplePosts()
    {
        Posts.Add(new FeedPost
        {
            AuthorName = "Teacher Sarah",
            Content = "The kids had a wonderful time painting today! 🎨",
            PostedAt = DateTime.Now.AddHours(-2),
            ImageUrl = null
        });
        Posts.Add(new FeedPost
        {
            AuthorName = "Teacher Mike",
            Content = "Story time was a big hit this afternoon. We read 'The Very Hungry Caterpillar'! 📚",
            PostedAt = DateTime.Now.AddHours(-5),
            ImageUrl = null
        });
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            // Simulate refresh
            await Task.Delay(1000);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddPostAsync()
    {
        if (string.IsNullOrWhiteSpace(NewPostContent)) return;

        try
        {
            IsBusy = true;
            await Task.Delay(300);

            Posts.Insert(0, new FeedPost
            {
                AuthorName = "You",
                Content = NewPostContent,
                PostedAt = DateTime.Now
            });

            NewPostContent = string.Empty;
        }
        finally
        {
            IsBusy = false;
        }
    }
}

public class FeedPost
{
    public string AuthorName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PostedAt { get; set; }
    public string? ImageUrl { get; set; }
    public string TimeAgo
    {
        get
        {
            var diff = DateTime.Now - PostedAt;
            if (diff.TotalMinutes < 1) return "Just now";
            if (diff.TotalHours < 1) return $"{(int)diff.TotalMinutes}m ago";
            if (diff.TotalDays < 1) return $"{(int)diff.TotalHours}h ago";
            return PostedAt.ToString("MMM d");
        }
    }
}
