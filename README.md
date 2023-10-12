# SplitPdf
Simple console app PDF splitter. Split one PDF file into multiple PDF file.

# How to use
You can use the console app in 2 ways:
- Start it without any parameters: the console will ask you the required parameters.
- Pass the required parameters when starting the console.

There are two parameters:
1. The file path.
2. The PDF pages to split on. Example: "1,3,5,6,7".

SplitPdf will:
1. Create a directory with the same name as the file.
2. Split the given PDF file into multiple files and put them into this directory.
Note: the files are named with the pages numbers. Example: "1-3.pdf"

# Important
It's a really simple console app. It means that for now there are no checks at all.
If the path is not valid, if it's not a pdf,... the app will just throw an exception and stop.

# Contribution
I created this console app for private use. But feel free to use it as you want.
You can also help by writing some issues and/or pull requests.
