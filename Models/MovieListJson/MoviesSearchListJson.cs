using MovieStore.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MoviesSearchListJson
    {

        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }


        //2 next properties added to save information for pagination
        public string searchString { get; set; }
        public string language { get; set; }


        //this is for setting the last result added to the page, whenever there is a search by language. 
        //For example, in a search of German language films, if the 20th result is the 243 result of the total search, that includes all other languages
        //the 243 will be stored here, to not repeat results for second page. 
        public int current_result { get; set; }
        //store the page for the JSON api call without messing with the page number for the view
        public int page_number_language { get; set; } = 0;



        public class Result
        {
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public List<int> genre_ids { get; set; }
            public int id { get; set; }
            public string original_language { get; set; }
            public string original_title { get; set; }
            public string overview { get; set; }
            public double popularity { get; set; }
            public string poster_path { get; set; }
            public string release_date { get; set; }
            public string title { get; set; }
            public bool video { get; set; }
            public double vote_average { get; set; }
            public int vote_count { get; set; }
        }


        public void PaginatedByLanguage (string language, int page_number_language, int current_result)
        {
            if (language == "en" || (language == null && this.language == null))
            {
                this.language = language;
            }
            else
            {
                this.current_result = current_result;
                this.page_number_language = page_number_language;
                //tests if this is the first page in the language search. If it is, takes all elements from the API call done in the contrller.
                //if not, retrieves the last page searched, takes away the elements already seen in the previous page, and does the rest of the process
                if(this.page_number_language == 0)
                {
                    this.results = this.results.FindAll(t => t.original_language == language);
                    this.page_number_language = this.page;
                }
                    
                else
                {
                    string args = JSONMethods.BuildSearchString(this.searchString, null, this.page_number_language);
                    string jsonStringMovieSearch = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/search/movie?api_key=5933922b6587d2d506362381025ef410"
                       + args);
                    MoviesSearchListJson newList = JsonConvert.DeserializeObject<MoviesSearchListJson>(jsonStringMovieSearch);
                    newList.results = newList.results.Skip(this.current_result).ToList();
                    this.results = newList.results.FindAll(t => t.original_language == language);
                }

                

                int remainingElements = 0;

                while(this.results.Count < 20 && this.page_number_language < total_pages)
                {
                    string args = JSONMethods.BuildSearchString(this.searchString, null, this.page_number_language + 1);
                    string jsonStringMovieSearch = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/search/movie?api_key=5933922b6587d2d506362381025ef410"
                       + args);
                    MoviesSearchListJson newList = JsonConvert.DeserializeObject<MoviesSearchListJson>(jsonStringMovieSearch);

                    var newResult = newList.results.Find(t => t.original_language == (language ?? this.language));
                    while(newResult != null && this.results.Count < 20)
                    {
                        this.results.Add(newResult);
                        newList.results = newList.results.SkipWhile(t => t.id != newResult.id).ToList();
                        newList.results.RemoveAt(0);
                        newResult = newList.results.Find(t => t.original_language == (language ?? this.language));

                        remainingElements = 20 - newList.results.Count;
                    }
                    this.page_number_language += 1;
                }
                this.current_result = remainingElements;
                this.language = language;
            }
            
        }
    }
}
