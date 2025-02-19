﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class SelectTableTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        var table = cut.FindComponent<SelectTable<Foo>>();
        Assert.Throws<InvalidOperationException>(() =>
        {
            table.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.OnQueryAsync, null);
            });
        });
    }

    [Fact]
    public void TableMinWidth_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.TableMinWidth, 300);
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        Assert.Contains("data-bb-min-width=\"300\"", cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Color, Color.Danger);
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
            });
        });
        cut.Contains("border-danger");
    }

    [Fact]
    public void ShowAppendArrow_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.ShowAppendArrow, false);
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        cut.DoesNotContain("form-select-append");
    }

    [Fact]
    public void Template_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.Template, foo => builder =>
                {
                    builder.AddContent(0, $"Template-{foo.Name}");
                });
            });
        });
        var rows = cut.FindAll("tbody > tr");
        Assert.Equal(4, rows.Count);

        var table = cut.FindComponent<SelectTable<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, items[0]);
        });
        Assert.Contains($"Template-{items[0].Name}", cut.Markup);
    }

    [Fact]
    public void GetTextCallback_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        Assert.Contains($"value=\"{items[0].Name}\"", cut.Markup);

        var table = cut.FindComponent<SelectTable<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.GetTextCallback, foo => null);
        });

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, null);
        });
        Assert.DoesNotContain("value=\"\"", cut.Markup);

        Assert.Throws<InvalidOperationException>(() =>
        {
            table.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.GetTextCallback, null);
            });
        });
    }

    [Fact]
    public void Height_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.Height, 100);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        Assert.Contains($"height: 100px;", cut.Markup);
    }

    [Fact]
    public async Task Value_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        Foo? v = null;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.OnValueChanged, foo =>
                {
                    v = foo;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var rows = cut.FindAll("tbody > tr");
        await cut.InvokeAsync(() =>
        {
            rows[1].Click();
        });
        Assert.Equal(items[1].Name, v?.Name);
    }

    [Fact]
    public async Task Validate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var valid = false;
        var invalid = false;
        var model = new SelectTableModel() { Foo = items[0] };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.OnValidSubmit, context =>
            {
                valid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.Model, model);
            builder.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Value, model.Foo);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Foo", typeof(Foo)));
                pb.Add(a => a.OnValueChanged, v =>
                {
                    model.Foo = v;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
                pb.Add(a => a.OnQueryAsync, options => OnFilterQueryAsync(options, items));
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(valid);

        model.Foo = null;
        var table = cut.FindComponent<SelectTable<Foo>>();
        table.SetParametersAndRender();
        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(invalid);
    }

    private Task<QueryData<Foo>> OnFilterQueryAsync(QueryPageOptions options, IEnumerable<Foo> _filterItems)
    {
        _filterItems = _filterItems.Where(options.ToFilter().GetFilterFunc<Foo>());

        if (!string.IsNullOrEmpty(options.SortName))
        {
            _filterItems = _filterItems.Sort(options.SortName, options.SortOrder);
        }
        return Task.FromResult(new QueryData<Foo>()
        {
            Items = _filterItems.ToList(),
            IsAdvanceSearch = true,
            IsFiltered = true,
            IsSearch = true,
            IsSorted = true
        });
    }

    class SelectTableModel()
    {
        public Foo? Foo { get; set; }
    }
}
