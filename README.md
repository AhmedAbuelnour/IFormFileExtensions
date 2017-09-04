# IFormFileExtensions
There are some missing features for IFormFile in Asp.Net Core, this library trying to implement them

# Getting started 
All you have to do to get started with the IFormFileExtensions is by installing the library from the Nuget  `Install-Package IFormFileExtensions`

# Extension Methods 

public async Task<IActionResult> PostFile(IFormFile file)
{	
	// To Read the file content 
	string FileContent = await file.ReadAllTextAsync(); 

	// To Convert a file to byte array
	byte[] FileBytes = await file.ConvertFileToBytesAsync();

	// To Store a file in the server, by giving directory path, even if it not exists, it will create it
	await file.StoreFileToServerFolderAsync("Images/username");

	// Compute the has code for a file
	string Hash40 = file.GetWeakHash40();
	string Hash64 = file.GetGetIntermediateHash64();
	string Hash128 = file.GetStrongHash128();


	// Encrypt a file 
	string EncryptedContent = await file.EncryptAsyn();

	// Decrypt a file 
	string DencryptedContent = await file.DencryptAsync();

	// To check if a certain file is actually is an image 
	ImageFormat  imageformat = file.GetImageFormat();
}
