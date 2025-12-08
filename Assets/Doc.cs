public class Doc {
  /*
================================================================================
                            Voro Terrain System
================================================================================

VoroWorld
---------
- The environment container where generated terrain is instantiated.

TileMap
-------
- Stores positional data for all Chunks.
- Serves as the environment map to be placed into the VoroWorld

Chunk
---------
- Base blueprint for terrain generation.
- Parses the point table and configuration that define the base terrain form.
- User-set effects are applied onto the point data in the chunk.

VoroUI
------
- This is what the user interacts with.
- Produces instructions for terrain generation

Diagram
-------
- Core structure storing terrain layout.
- Receives cell point array from Chunk.
- Receives dictionary from Editor.
- Outputs positional+mesh data for all Tiles.

VoroGeneration
--------------
- Oversees the entire terrain generation process.
- Acts as the central control class of the system.

VoroCompute
-----------
- Executes terrain generation.
- Generates the actual results based on Diagram instructions.

================================================================================
*/
  /*
   * public class BookList
     {
       public string Author { get; set; }
       public string Title { get; set; }
     }

     class MyTest
     {
       private void Test(List<Book> books)
       {
         var bookList = from book in books select new BookList
             {Author = book.Author, Title = book.Title};
       }
     }

     class MyNewTest
     {
       private void Foo(List<Book> library)
       {
         var bookCatalog = from item in library select new BookList
             {Author = item.Author, Title = item.Title};
       }
     }
   */
  /*
   * class TestBookLibrary
     {
       Book[] _books;

       Book this[int index]
       {
         get { return _books[index]; }
       }

       void Insert(int index, Book book)
       {
         _books[index] = book;
       }

       void Copy(int copy, int to)
       {
         Insert(to, this[copy]);
       }
     }
   */
}