open System

[<Sealed>]
type Book(title: string, author: string, pages: int) =
    member val Title = title with get
    member val Author = author with get
    member val Pages = pages with get
    member this.GetInfo() = sprintf "%s by %s, %d pages" this.Title this.Author this.Pages

[<Sealed>]
type User(name: string) =
    let mutable borrowedBooks = []
    
    member val Name = name with get
    
    member this.BorrowBook(book: Book) =
        borrowedBooks <- book :: borrowedBooks
        printfn "%s borrowed: %s" this.Name (book.GetInfo())
    
    member this.ReturnBook(book: Book) =
        borrowedBooks <- List.filter ((<>) book) borrowedBooks
        printfn "%s returned: %s" this.Name (book.GetInfo())
    
    member this.ListBorrowedBooks() =
        if List.isEmpty borrowedBooks then
            printfn "%s has no borrowed books." this.Name
        else
            printfn "%s has borrowed the following books:" this.Name
            borrowedBooks |> List.iter (fun book -> printfn " - %s" (book.GetInfo()))

[<Sealed>]
type Library() =
    let mutable books = []
    
    member this.AddBook(book: Book) =
        books <- book :: books
        printfn "Added: %s" (book.GetInfo())
    
    member this.RemoveBook(book: Book) =
        books <- List.filter ((<>) book) books
        printfn "Removed: %s" (book.GetInfo())
    
    member this.ListBooks() =
        if List.isEmpty books then
            printfn "No books available in the library."
        else
            printfn "Books available in the library:"
            books |> List.iter (fun book -> printfn " - %s" (book.GetInfo()))
    
    member this.BorrowBook(user: User, book: Book) =
        if List.contains book books then
            this.RemoveBook(book)
            user.BorrowBook(book)
        else
            printfn "The book '%s' is not available in the library." book.Title
    
    member this.ReturnBook(user: User, book: Book) =
        user.ReturnBook(book)
        this.AddBook(book)

[<EntryPoint>]
let main argv =
    let library = Library()
    let user = User("John")
    
    let book1 = Book("Book1", "A. U. Stone", 253)
    let book2 = Book("Book2", "G. Smith", 233)
    
    library.AddBook(book1)
    library.AddBook(book2)
    
    library.ListBooks()
    
    library.BorrowBook(user, book1)
    user.ListBorrowedBooks()
    library.ListBooks()
    
    library.ReturnBook(user, book1)
    user.ListBorrowedBooks()
    library.ListBooks()
    
    0