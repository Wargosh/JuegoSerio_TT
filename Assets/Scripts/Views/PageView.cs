using System;
using UnityEngine;

/// <summary>
/// Table of contents page.
/// Handles clicks on the chapters to jump to pages in the book
/// </summary>
public class PageView : PageViewController
{
    /// <summary>
    /// The name of the collider and what page number
    /// it is associated with
    /// </summary>
    [Serializable]
    public struct ChapterJump
    {
        public string gameObjectName;
        public int pageNumber;
    }

    public ChapterJump[] chapterJumps;

    protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
    {
        if (action == null) return false;

        foreach (var chapterJump in chapterJumps)
        {
            if (chapterJump.gameObjectName == hit.collider.gameObject.name)
            {
                action(BookActionTypeEnum.TurnPage, chapterJump.pageNumber);
                return true;
            }
        }
        return false;
    }
}