﻿using FastWiki.Service.Backgrounds;
using FastWiki.Service.Service;

namespace FastWiki.Service.Application.Wikis;

public sealed class WikiCommandHandler(
    IWikiRepository wikiRepository,
    WikiMemoryService wikiMemoryService,
    MemoryServerless memoryServerless,
    IMapper mapper,
    IEventBus eventBus)
{
    [EventHandler]
    public async Task CreateWiki(CreateWikiCommand command)
    {
        var wiki = new Wiki(command.Input.Icon, command.Input.Name, command.Input.Model, command.Input.EmbeddingModel);
        await wikiRepository.AddAsync(wiki);
    }

    [EventHandler]
    public async Task RemoveWiki(RemoveWikiCommand command)
    {
        var wikiDetailsQuery = new WikiDetailsQuery(command.Id, string.Empty, 1, int.MaxValue);

        await eventBus.PublishAsync(wikiDetailsQuery);

        await wikiRepository.RemoveAsync(command.Id);

        var ids = wikiDetailsQuery.Result.Result.Select(x => x.Id).ToList();

        await wikiRepository.RemoveDetailsAsync(ids);

        foreach (var id in ids)
        {
            try
            {
                await memoryServerless.DeleteDocumentAsync(id.ToString(), "wiki");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    [EventHandler]
    public async Task CreateWikiDetailsAsync(CreateWikiDetailsCommand command)
    {
        var wikiDetail = new WikiDetail(command.Input.WikiId, command.Input.Name, command.Input.FilePath,
            command.Input.FileId, 0, "file");

        wikiDetail = await wikiRepository.AddDetailsAsync(wikiDetail);

        QuantizeWikiDetail quantizeWikiDetail = mapper.Map<QuantizeWikiDetail>(wikiDetail);
        quantizeWikiDetail.Subsection = command.Input.Subsection;
        quantizeWikiDetail.Mode = command.Input.Mode;
        quantizeWikiDetail.TrainingPattern = command.Input.TrainingPattern;

        await QuantizeBackgroundService.AddWikiDetailAsync(quantizeWikiDetail);
    }

    [EventHandler]
    public async Task CreateWikiDetailWebPageAsync(CreateWikiDetailWebPageCommand command)
    {
        var wikiDetail = new WikiDetail(command.Input.WikiId, command.Input.Name, command.Input.Path,
            -1, 0, "web");

        wikiDetail = await wikiRepository.AddDetailsAsync(wikiDetail);

        var quantizeWikiDetail = mapper.Map<QuantizeWikiDetail>(wikiDetail);
        quantizeWikiDetail.Subsection = command.Input.Subsection;
        quantizeWikiDetail.Mode = command.Input.Mode;
        quantizeWikiDetail.TrainingPattern = command.Input.TrainingPattern;

        await QuantizeBackgroundService.AddWikiDetailAsync(quantizeWikiDetail);
    }

    [EventHandler]
    public async Task CreateWikiDetailDataAsync(CreateWikiDetailDataCommand command)
    {
        var wikiDetail = new WikiDetail(command.Input.WikiId, command.Input.Name, command.Input.FilePath,
                       command.Input.FileId, 0, "data");

        wikiDetail = await wikiRepository.AddDetailsAsync(wikiDetail);

        var quantizeWikiDetail = mapper.Map<QuantizeWikiDetail>(wikiDetail);
        quantizeWikiDetail.Subsection = command.Input.Subsection;
        quantizeWikiDetail.Mode = command.Input.Mode;
        quantizeWikiDetail.TrainingPattern = command.Input.TrainingPattern;

        await QuantizeBackgroundService.AddWikiDetailAsync(quantizeWikiDetail);
    }

    [EventHandler]
    public async Task RemoveWikiDetailsCommand(RemoveWikiDetailsCommand command)
    {
        await wikiRepository.RemoveDetailsAsync(command.Id);

        try
        {
            await memoryServerless.DeleteDocumentAsync(command.Id.ToString(), "wiki");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [EventHandler]
    public async Task RemoveWikiDetailVectorQuantityAsync(RemoveWikiDetailVectorQuantityCommand command)
    {
        await memoryServerless.DeleteDocumentAsync(command.DocumentId, "wiki");
    }

    [EventHandler]
    public async Task RemoveDetailsVectorAsync(RemoveDetailsVectorCommand command)
    {
        await wikiRepository.RemoveDetailsVectorAsync("wiki", command.Id);
    }

    [EventHandler]
    public async Task UpdateWikiAsync(UpdateWikiCommand command)
    {
        var wiki = mapper.Map<Wiki>(command.Dto);
        await wikiRepository.UpdateAsync(wiki);
    }
}