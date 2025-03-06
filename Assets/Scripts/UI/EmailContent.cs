using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailContent : ContentPopulation
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Email email in EmailManager.instance.emails)
        {
            ContentBlock block = Instantiate(contentBlock, transform).GetComponent<ContentBlock>();
            block.GetComponent<EmailContentBlock>().SetEmail(email);
            contentBlocks.Add(block);
        }
    }
}
