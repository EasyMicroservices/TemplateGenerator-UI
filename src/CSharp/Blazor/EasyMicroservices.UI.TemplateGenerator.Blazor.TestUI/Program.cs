using EasyMicroservices.Domain.Contracts.Common;
using EasyMicroservices.UI.BlazorComponents;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.TemplateGenerator.Blazor.TestUI;
using EasyMicroservices.UI.TemplateGenerator.ViewModels.FormItems;
using EasyMicroservices.UI.TemplateGenerator.ViewModels.Forms;
using EasyMicroservices.UI.TemplateGenerator.ViewModels.NoParentFormItems;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using TemplateGenerators.GeneratedServices;

LoadLanguage("en-US");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
string baseAddress = "http://localhost:1050";
//string baseAddress = "https://templategenerator.adahmsay.ir/";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new FormClient(baseAddress, sp.GetService<HttpClient>()));
builder.Services.AddScoped(sp => new NoParentFormItemClient(baseAddress, sp.GetService<HttpClient>()));

builder.Services.AddTransient<FilterFormsListViewModel>();
builder.Services.AddTransient<AddOrUpdateFormViewModel>();
builder.Services.AddTransient<AddOrUpdateFormItemViewModel>();
builder.Services.AddTransient<DialogBaseViewModel>();
builder.Services.AddTransient<FilterNoParentFormItemsListViewModel>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});

await builder.Build().RunAsync();

void LoadLanguage(string languageShortName)
{
    BaseViewModel.CurrentApplicationLanguage = languageShortName;
    BaseViewModel.AppendLanguage("Saving", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Saving"
    });
    BaseViewModel.AppendLanguage("Save", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Save"
    });
    BaseViewModel.AppendLanguage("Search", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Search"
    });
    BaseViewModel.AppendLanguage("Processing", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Processing"
    });
    BaseViewModel.AppendLanguage("Id", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Id"
    });
    BaseViewModel.AppendLanguage("Name", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Name"
    });
    BaseViewModel.AppendLanguage("Actions", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Actions"
    });
    BaseViewModel.AppendLanguage("Delete", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Delete"
    });
    BaseViewModel.AppendLanguage("Deleting", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Deleting"
    });
    BaseViewModel.AppendLanguage("Orders", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Orders"
    });
    BaseViewModel.AppendLanguage("Amount", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Amount"
    });
    BaseViewModel.AppendLanguage("Cancel", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Cancel"
    });
    BaseViewModel.AppendLanguage("DeleteQuestion_Content", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Do you really want to delete these records? This process cannot be undone."
    }); 
    
    BaseViewModel.AppendLanguage("Forms", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Forms"
    });

    BaseViewModel.AppendLanguage("AddForm_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Add Forms"
    });

    BaseViewModel.AppendLanguage("DeleteForm_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Delete Form"
    });

    BaseViewModel.AppendLanguage("DeleteForm_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Form deleted!"
    });

    BaseViewModel.AppendLanguage("Index", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Index"
    });

    BaseViewModel.AppendLanguage("FormItems", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Form Items"
    });

    BaseViewModel.AppendLanguage("Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Title"
    });

    BaseViewModel.AppendLanguage("FormItemType", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Type"
    });

    BaseViewModel.AppendLanguage("DefaultValue", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Default Value"
    });

    BaseViewModel.AppendLanguage("DeleteFormItem_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Delete Form Item"
    });

    BaseViewModel.AppendLanguage("UpdateForm_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Update Form"
    });

    BaseViewModel.AppendLanguage("UpdateForm_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Form updated!"
    });

    BaseViewModel.AppendLanguage("NoParentFormItems", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Form Items"
    });

    BaseViewModel.AppendLanguage("AddNoParentFormItem_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Add Form Items"
    });
    BaseViewModel.AppendLanguage("TextBox", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "TextBox"
    });
    BaseViewModel.AppendLanguage("CheckList", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "CheckList"
    });
    BaseViewModel.AppendLanguage("CheckBox", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "CheckBox"
    });
    BaseViewModel.AppendLanguage("OptionList", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "OptionList"
    });
    BaseViewModel.AppendLanguage("DateTime", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "DateTime"
    });
    BaseViewModel.AppendLanguage("DateOnly", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "DateOnly"
    });
    BaseViewModel.AppendLanguage("TimeOnly", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "TimeOnly"
    });
    BaseViewModel.AppendLanguage("Label", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Label"
    });
    BaseViewModel.AppendLanguage("Table", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Table"
    });
    BaseViewModel.AppendLanguage("Row", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Row"
    });
    BaseViewModel.AppendLanguage("AutoIncrementNumber", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "AutoIncrementNumber"
    });
    BaseViewModel.AppendLanguage("Column", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Column"
    });
    BaseViewModel.AppendLanguage("Menu", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Menu"
    });
    BaseViewModel.AppendLanguage("Card", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Card"
    });
}