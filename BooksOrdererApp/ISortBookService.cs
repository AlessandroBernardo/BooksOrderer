using BooksOrdererDomain;

using System.Collections.Generic;

namespace BooksOrdererApp
{
    public interface ISortBookService
    {
        List<Book> OrderBooks(Dictionary<string, string> parameters);
    }
}
