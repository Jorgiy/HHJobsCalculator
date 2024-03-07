# HH Jobs Calculator

Test task. Create an engine for costs calculations of print items provided e.g. some goods with their netto price.  
Engine apply margins and taxes to all goods provided, presenting detailed info about prices with tax and total gross cost of the enire basket.

## Calculation logic

Each run of calculation is called a 'job'. Job contains print items with prices and tax extempt option. Tax value is 7% (in case of exptempt is 0%).
Also job contains increased margin option. If checked, margin value will be 16% otherwise 11%. 
Final cost of each print item is a sum of its nettp price, tax added and margin added.
Engine generate response which consists of each item with its tax added price calculated, rounded to nearest cent and total price of the entire bucket, 
rounded to nearest even cent (!it is not an even half 'banker' rounding by itself).

## Build

Project is targetet to .netstandard2.1 and .net 6. You need to install .net6 SDK to build this solution.
There are two publish profiles in HHJobsCalculator.WebApi project: ```FolderProfile.pubxml``` which generates self conatined binaries and no runtime installed nedded
and ```FolderProfile1.pubxml``` which generates binaries for .net 6 and ASP.NET Core 6.0 runtimes installed.
Both configurations generate executable ```HHJobsCalculator.WebApi.exe``` which instructs about port bindings in cosole. !Usage of HTTP is not intended, only HTTPS.

## API

REST API is porvied to interact with an engine. It is described via OpenAPI. You just need to proceed with ```/swagger``` endpoint which provides interaction interface,
model samples, models and endpoint descriptions, which are self-descriptive.

## Docker

Project could be puclished to a docker container. Code are compatible with Linux platforms.
To create docker image it is just need to run ```docker build -t hhjobscalculator -f %full folder path to \HHJobsCalculator.WebApi\Dockerfile file% .``` from the root solution folder (!).
To compose a docker container you need to firstly setup a Kerberos developer self-signed certificate: [Instruction](https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0).
Then substitute ```%password_placeholder%``` in ```docker-compose.yml``` with an actual self-signed secret and run ```docker-compose -f "docker-compose.yml" up -d``` from the HHJobsCalculator.WebApi folder. 