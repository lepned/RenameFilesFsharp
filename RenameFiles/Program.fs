open System
open System.IO

let imagepath = """C:\Users\lepne_000\Desktop\Bilder"""

let rec getAllFiles path = 
    [ yield! Directory.GetFiles(path)
      for dir in Directory.GetDirectories(path) do
          yield! getAllFiles dir ]

let onlyJpgFiles = getAllFiles imagepath |> List.filter (fun name -> Path.GetExtension(name.ToLower()) = ".jpg")

printfn "Totalt antall .jpg - filer =: %d" onlyJpgFiles.Length

let renameString str = 
    let filename = Path.GetFileNameWithoutExtension(str)
    let ext = Path.GetExtension(str)
    let newname = filename + ext.ToLower()
    let dir = Path.GetDirectoryName(str)
    Path.Combine(dir, newname)

//uten Path funksjonalitet...
let renamStr (path : string) = 
    let lengde = path.Length - 1
    let utenExt = path.Substring(0, lengde - 3)
    let ext = path.Substring(lengde - 3, 4)
    utenExt + ext.ToLower()

let renameAll = 
    onlyJpgFiles |> List.iter (fun name -> 
                        if Path.GetExtension(name) = ".jpg" then printfn "File is ok: %s" name
                        else 
                            let res = renameString name
                            //printfn "File needs to be renamed: from:\n %s to:\n %s:" name res
                            File.Move(name, res))

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
