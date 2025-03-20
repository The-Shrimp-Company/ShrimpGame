using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ShrimpSelectionPopulation : ContentPopulation
{
    private Request _request;
    private EmailScreen _window;
    private Email _email;

    public void Populate(Request request, EmailScreen window)
    {
        _request = request;
        _window = window;
        _email = request.email;
        List<Shrimp> shrimp = ShrimpManager.instance.allShrimp;
        if (request.obfstats.tail.obfuscated)
        {
            shrimp = shrimp.Where(x => x.stats.tail.activeGene.ID == request.stats.tail.activeGene.ID).ToList();
        }
        if (request.obfstats.primaryColour.obfuscated)
        {
            shrimp = shrimp.Where(x => x.stats.primaryColour.activeGene.ID == request.stats.primaryColour.activeGene.ID).ToList();
        }
        if (request.obfstats.tailFan.obfuscated)
        {
            shrimp = shrimp.Where(x => x.stats.tailFan.activeGene.ID == request.stats.tailFan.activeGene.ID).ToList();
        }
        if (request.obfstats.body.obfuscated)
        {
            shrimp = shrimp.Where(x => x.stats.body.activeGene.ID == request.stats.body.activeGene.ID).ToList();
        }
        if (request.obfstats.secondaryColour.obfuscated)
        {
            shrimp = shrimp.Where(x => x.stats.secondaryColour.activeGene.ID == request.stats.secondaryColour.activeGene.ID).ToList();
        }
        if (request.obfstats.eyes.obfuscated)
        {
            shrimp = shrimp.Where(x => x.stats.eyes.activeGene.ID == request.stats.eyes.activeGene.ID).ToList();
        }
        if (request.obfstats.pattern.obfuscated)
        {
            shrimp = shrimp.Where(x => x.stats.pattern.activeGene.ID == request.stats.pattern.activeGene.ID).ToList();
        }
        if (request.obfstats.legs.obfuscated)
        {
            shrimp = shrimp.Where(x => x.stats.legs.activeGene.ID == request.stats.legs.activeGene.ID).ToList();
        }

        foreach(Shrimp s in shrimp)
        {
            GameObject block = Instantiate(contentBlock, transform);
            block.GetComponent<ShrimpSelectionBlock>().Populate(s.stats);
            contentBlocks.Add(block.GetComponent<ContentBlock>());
            s.currentValue = EconomyManager.instance.GetObfsShrimpValue(request.obfstats);
            block.GetComponent<Button>().onClick.AddListener(s.SellThis);
            block.GetComponent<Button>().onClick.AddListener(CompleteRequest);
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

    public void CompleteRequest()
    {
        CustomerManager.Instance.CompleteRequest(_request);
        foreach(Email email in EmailManager.instance.emails)
        {
            if(email.mainText == _email.mainText)
            {
                EmailManager.instance.emails.Remove(email);
                break;
            }
        }
        _window.CloseSelection();
    }

    public void CloseScreen()
    {
        _window.CloseSelection();
    }
}
