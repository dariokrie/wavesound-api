## WaveSound Web Api
- Based on a RESTful web api (ASP.NET MVC)
- SwaggerUI as Interface
- Object to Object mapping via AutoMapper
- Written in .NET 8

## Architecture
10 Api
- WaveSound.Api
	- Controller
		-> Api endpoints are defined here
	- Models
		-> request and response models
	- Profiles
		-> profiles for automapper
		
20 Domain
- WaveSound.Domain
	- Models
		-> each entity designed as a domain model
	- Services
		-> business logic
		
30 Common
- WaveSound.Common
	- Exceptions
		-> custom exception
	- Extension
		-> contains a static class, extension method included

40 Testing
- WaveSound.UnitTest
	- Tests: PathEx, SoundCloudService, SpotifyService
		
## Techstack
- C# .NET8

## Information
Commits and coding history is not tracked on github. The origin of this repository is Azure Devops Repos.
