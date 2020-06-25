# BWTool
 A c# cracker tool for bitcoin brainwallets and other low entropy key algorithms.
 The random string and password cracking tools are working now.
 Now you can search keys in a key range, decimal and hex numbers can be used search.
 You can even choose that you want to search incrementally or you want to decrement.
 
 
 Usage:
 Load a txt file with passwords, load a txt file containing new line separated bitcoin addresses, and you can mine instantly.
 Options: 
 - You can choose your own separator instead of newline ,
 - Sha256 hashing can be repeated on strings,
 - Lookup possible on compressed and uncompressed addresses as well.
 You can brute force now passwords from costume char pool and costume length. Just check the using random chars checkbox and click start mining. If the using user selected chars textbox is empty a predefined char array will be used.
 
 Key range search:
 - Use a hex or decimal number in the start from textbox and a bigger number in the until textbox if you want to search incrementally.
 -If you using a hex number you should check the using hexadecimal checkbox first.
 -If you placed a number in any textbox it will be converted if you change the hexadecimal checkbox.
 -Finally just load an address list and hit start mining button.
 
 https://i.imgur.com/cUUJ836.jpg
 
 Expect to see:
 - blockchain parser tab
 - more address related helpers
 
 You can download a test address txt file here with 24+ million distinct addresses:
 
 https://drive.google.com/file/d/1oJ-EMLcOghgMIpwgQKjuzYorKMD1SjqH/view?usp=sharing
 
