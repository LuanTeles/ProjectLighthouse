using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Types.Entities.Level;
using LBPUnion.ProjectLighthouse.Types.Levels;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Types.Categories;

public static class CategoryHelper
{
    public static readonly List<Category> Categories = new();

    static CategoryHelper()
    {
        Categories.Add(new TeamPicksCategory());
        Categories.Add(new MostHeartedCategory());
        Categories.Add(new NewestLevelsCategory());
        Categories.Add(new MostPlayedCategory());
        Categories.Add(new HighestRatedCategory());
        Categories.Add(new QueueCategory());
        Categories.Add(new HeartedCategory());
        Categories.Add(new LuckyDipCategory());

        using DatabaseContext database = new();
        foreach (DatabaseCategory category in database.CustomCategories) Categories.Add(new CustomCategory(category));
    }
}