using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorClientApp.Components;

public class Menu : ComponentBase
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        builder.OpenElement(0,"nav");
        builder.AddAttribute(1, "class", "menu");

        builder.OpenElement(2, "ul");

        builder.OpenElement(3, "li");

        builder.OpenComponent<NavLink>(4);
        builder.AddAttribute(5, "href", "/");
        builder.AddAttribute(6, "ChildContent", (RenderFragment) (builder2 =>
        {
            builder2.AddContent(7, "Home");
        }));

        builder.CloseComponent();

        builder.CloseElement();

        builder.CloseElement();

        builder.CloseElement();

    }
}
