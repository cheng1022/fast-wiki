﻿namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class WikiInfo
{
    [Parameter] public long Id { get; set; }

    private PaginatedListBase<WikiDetailDto> _wikiDetails = new();
    private WikiDto Wiki { get; set; }
    private int page = 1;
    private int pageSize = 10;

    private string keyword = string.Empty;

    private readonly List<DataTableHeader<WikiDetailDto>> _headers =
    [
        new()
        {
            Text = "#",
            Sortable = false,
            Value = nameof(WikiDetailDto.Id)
        },

        new()
        {
            Text = "文件名",
            Sortable = false,
            Value = nameof(WikiDetailDto.FileName)
        },

        new()
        {
            Text = "索引数量",
            Sortable = false, Value = nameof(WikiDetailDto.DataCount)
        },

        new()
        {
            Text = "数据类型",
            Sortable = false, Value = nameof(WikiDetailDto.Type)
        },
        new()
        {
            Text = "数据状态",
            Sortable = false, Value = nameof(WikiDetailDto.State)
        },

        new()
        {
            Text = "创建时间",
            Sortable = false, Value = nameof(WikiDetailDto.CreationTime)
        },

        new()
        {
            Text = "操作",
            Sortable = false,
        }
    ];

    private async Task LoadData()
    {
        _wikiDetails = await WikiService.GetWikiDetailsAsync(Id, keyword, page, pageSize);
        _ = InvokeAsync(StateHasChanged);
    }
    
    private async Task Remove(long id)
    {
        await WikiService.RemoveDetailsAsync(id);
        page = 1;
        await LoadData();
    }

    private async Task OnPageChanged(int page)
    {
        if (this.page == page)
        {
            return;
        }
        this.page = page;
        await LoadData();
    }

    private void OpenWikiDetailAsync(WikiDetailDto item)
    {
        NavigationManager.NavigateTo("/wiki/wiki-detail/" + item.Id);
    }

    private async Task OnSucceed(bool result)
    {
        if (result)
        {
            page = 1;
            await LoadData();
        }
    }

    private async Task OnSearchAsync()
    {
        page = 1;
        await LoadData();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadData();
            Wiki = await WikiService.GetAsync(Id);
        }
    }
}