using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShrimpSelectionPopulation : ContentPopulation
{
    // Start is called before the first frame update
    void Start()
    {
        //CreateContent();
    }

    public void Populate(Request request)
    {
        List<ShrimpStats> shrimp = ShrimpManager.instance.allShrimp.Select(x => x.stats).ToList();
        if (!request.obfstats.tail.obfuscated)
        {
            shrimp = shrimp.Where(x => x.tail.activeGene.ID == request.stats.tail.activeGene.ID).ToList();
        }
        if (!request.obfstats.primaryColour.obfuscated)
        {
            shrimp = shrimp.Where(x => x.primaryColour.activeGene.ID == request.stats.primaryColour.activeGene.ID).ToList();
        }
        if (!request.obfstats.tailFan.obfuscated)
        {
            shrimp = shrimp.Where(x => x.tailFan.activeGene.ID == request.stats.tailFan.activeGene.ID).ToList();
        }
        if (!request.obfstats.body.obfuscated)
        {
            shrimp = shrimp.Where(x => x.body.activeGene.ID == request.stats.body.activeGene.ID).ToList();
        }
        if (!request.obfstats.secondaryColour.obfuscated)
        {
            shrimp = shrimp.Where(x => x.secondaryColour.activeGene.ID == request.stats.body.activeGene.ID).ToList();
        }
        if (!request.obfstats.eyes.obfuscated)
        {
            shrimp = shrimp.Where(x => x.eyes.activeGene.ID == request.stats.eyes.activeGene.ID).ToList();
        }
        if (!request.obfstats.pattern.obfuscated)
        {
            shrimp = shrimp.Where(x => x.pattern.activeGene.ID == request.stats.pattern.activeGene.ID).ToList();
        }
        if (!request.obfstats.legs.obfuscated)
        {
            shrimp = shrimp.Where(x => x.legs.activeGene.ID == request.stats.legs.activeGene.ID).ToList();
        }

        foreach(ShrimpStats s in shrimp)
        {
            GameObject block = Instantiate(contentBlock, transform);
            block.GetComponent<ShrimpSelectionBlock>().Populate(s);
            contentBlocks.Add(block.GetComponent<ContentBlock>());
        }
    }

    protected void CreateContent()
    {
        foreach ( Shrimp shrimp in ShrimpManager.instance.allShrimp)
        {
            GameObject block = Instantiate(contentBlock, transform);
            block.GetComponent<ShrimpSelectionBlock>().Populate(shrimp.stats);
            contentBlocks.Add(block.GetComponent<ContentBlock>());
        }
    }
}
