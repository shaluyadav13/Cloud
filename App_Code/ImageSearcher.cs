using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cite.DomainAuthentication;

/// <summary>
/// Summary description for ImageSearcher
/// </summary>
public static class ImageSearcher
{
    private struct SearchEntry
    {
        public Images image;
        public double value;
    }

    public static IEnumerable<Images> SearchAllImages(String searchTerm)
    {
        DBDataContext db = DBDataContext.CreateInstance();

        String[] terms = searchTerm.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        // Get a collection of images from the database that contain at least one of the terms in their title or description or author or uploadedBy
        List<Images> images = new List<Images>();

        foreach (String term in terms)
        {
            string lowerterm = term.ToLower();
            images.AddRange(db.Images.AsEnumerable().Where(x => x.Username.ToLower().Contains(lowerterm) || x.Title.ToLower().Contains(lowerterm) || x.Description.ToLower().Contains(lowerterm)));
            
        }

        // Now use our application's search method on the results.
        return SearchImages(images, searchTerm);
    }

    /// <summary>
    /// This method searches a collection of images, using a provided search term, and returns an ordered
    /// collection of images which the search applies to. The higher ranked results are at the top.
    /// </summary>
    /// <param name="files"></param>
    /// <param name="searchTerm"></param>
    /// <returns></returns>
    public static IEnumerable<Images> SearchImages(IEnumerable<Images> images, String searchTerm)
    {
        String[] terms = searchTerm.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        List<SearchEntry> search = new List<SearchEntry>();
        foreach (var image in images)
        {
            SearchEntry entry = new SearchEntry();
            entry.image = image;
            entry.value = 0;
            String[] author = null;
            // String[] uploadedBy = null;

            // Get the owner of the image.
            DomainAccount aidAccount = new DomainAccount(image.Username);

            // Break title and description into string arrays.
            String[] title = image.Title.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            String[] desc = image.Description.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

            //Break author into string arrays.
            if (!string.IsNullOrEmpty(image.Author))
            {
                author = image.Author.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            }
            //Break uploadedBy into string arrays.
            //if (!string.IsNullOrEmpty(video.UploadedBy))
            //{
            //    uploadedBy = video.UploadedBy.ToLower().Split(new String[] { "\t", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            //}
            // Compare each word in the search term to each word in the title, description, author and uploadedBy
            for (int i = 0; i < terms.Length; i++) // Outer loop for search term words.
            {
                for (int k = 0; k < title.Length; k++) // Inner loop over title words.
                {
                    if (title[k].Contains(terms[i]))
                    {
                        // First word matches. Begin comparing next words for as long as possible.
                        // We will weight matching sequences of words higher than single word matches.
                        int length = 1;
                        int tempi = i;
                        int tempk = k;
                        while (++tempi < terms.Length && ++tempk < title.Length && terms[tempi] == title[tempk])
                        {
                            length++;
                        }
                        // Title is weighted more than description.
                        // Square length to put a lot of emphasis on longer matching sequences.
                        entry.value += length * length * 2;
                    }
                } // End of title inner loop.

                for (int k = 0; k < desc.Length; k++) // Inner loop over description words.
                {
                    if (desc[k].Contains(terms[i]))
                    {
                        // First word matches. Begin comparing next words for as long as possible.
                        // We will weight matching sequences of words higher than single word matches.
                        int length = 1;
                        int tempi = i;
                        int tempk = k;
                        while (++tempi < terms.Length && ++tempk < desc.Length && terms[tempi] == desc[tempk])
                        {
                            length++;
                        }
                        // Square length to put a lot of emphasis on longer matching sequences.
                        entry.value += length * length;
                    }
                } // End of description inner loop.

                //author
                if (author != null)
                {
                    for (int k = 0; k < author.Length; k++) // Inner loop over author words.
                    {
                        if (author[k].Contains(terms[i]))
                        {
                            // First word matches. Begin comparing next words for as long as possible.
                            // We will weight matching sequences of words higher than single word matches.
                            int length = 1;
                            int tempi = i;
                            int tempk = k;
                            while (++tempi < terms.Length && ++tempk < author.Length && terms[tempi] == author[tempk])
                            {
                                length++;
                            }
                            // Author is weighted more than description.
                            // Square length to put a lot of emphasis on longer matching sequences.
                            entry.value += length * length * 2;
                        }
                    }
                } // End of author inner loop.

                //UploaedBy
                //if (uploadedBy != null)
                //{
                //    for (int k = 0; k < uploadedBy.Length; k++) // Inner loop over author words.
                //    {
                //        if (terms[i] == uploadedBy[k])
                //        {
                //            // First word matches. Begin comparing next words for as long as possible.
                //            // We will weight matching sequences of words higher than single word matches.
                //            int length = 1;
                //            int tempi = i;
                //            int tempk = k;
                //            while (++tempi < terms.Length && ++tempk < uploadedBy.Length && terms[tempi] == uploadedBy[tempk])
                //            {
                //                length++;
                //            }
                //            // UploaedeBy is weighted more than description.
                //            // Square length to put a lot of emphasis on longer matching sequences.
                //            entry.value += length * length * 2;
                //        }
                //    }
                //} // End of UploadedBy inner loop.

                // Now compare to the owner of the video.
                if (terms[i] == aidAccount.Username.ToLower())
                {
                    entry.value += 2;
                }
                if (terms[i] == aidAccount.FirstName.ToLower())
                {
                    entry.value += 2;
                }
                if (terms[i] == aidAccount.LastName.ToLower())
                {
                    entry.value += 2;
                }

            } // End of outer loop.

            if (entry.value > 0)
            {
                search.Add(entry);
            }
        } // End of foreach website loop.

        // Sort by the search values.
        search = search.OrderBy(i => -i.value).ToList();

        // Return results.
        return from i in search
               select i.image;
    }
}