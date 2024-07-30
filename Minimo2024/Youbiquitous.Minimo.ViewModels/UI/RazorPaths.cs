///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

namespace Youbiquitous.Minimo.ViewModels.UI;

public static class RazorPaths
{
    public static string Layouts => "/views/layouts";
    public static string Pages => "/views/pages";

    /// <summary>
    /// Compose a layout name given the custom folder structure of this project
    /// (No initial slash in the [path] parameter) and no trailing .cshtml
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Layout(string path)
    {
        return $"{Layouts}/{path}.cshtml";
    }

    /// <summary>
    /// Compose a shared/layout name given the custom folder structure of this project
    /// (No initial slash in the [path] parameter) and no trailing .cshtml
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string SharedLayout(string path)
    {
        return $"{Layouts}/shared/{path}.cshtml";
    }

    /// <summary>
    /// Compose a page name given the custom folder structure of this project
    /// (No initial slash in the [path] parameter) and no trailing .cshtml
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Page(string path)
    {
        return $"{Pages}/{path}.cshtml";
    }

    /// <summary>
    /// Compose a shared/page name given the custom folder structure of this project
    /// (No initial slash in the [path] parameter) and no trailing .cshtml
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string SharedPage(string path)
    {
        return $"{Pages}/shared/{path}.cshtml";
    }
}