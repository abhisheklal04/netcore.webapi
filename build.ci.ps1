$ErrorActionPreference='Stop'
docker build -t netcore.webapi .
if($LastExitCode -ne 0){throw "Could not build docker image"}
#start-process docker "run --rm -it -p 5000:80 netcore.webapi"
write-host built
