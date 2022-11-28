rootFolder=$(dirname $0)
cmfProject=$rootFolder/src

start_compose()
{
    run_cmd "docker compose -f $rootFolder/deployment/docker/compose/compose.yaml up"
}

# account commands

create_migration()
{
    local projectPath=$cmfProject/$1
    echo "Creating migration for $projectPath"

    run_cmd "dotnet ef migrations add $2 --project $projectPath/$1.Persistence --startup-project $projectPath/$1.Api"
}

remove_migration()
{
    local projectPath=$cmfProject/$1
    echo "Creating migration for $projectPath"
    set -o allexport
    source .env
    set +o allexport
    run_cmd "dotnet ef migrations rm $2 --project $projectPath/$1.Persistence --startup-project $projectPath/$1.Api"
}

run()
{
    export ASPNETCORE_URLS="http://localhost:$3"
    set -o allexport
    source .env
    set +o allexport
    run_cmd "dotnet run --project $cmfProject/$1/$1.$2"
}

run_cmd()
{
    echo "CMD: $1"
    $1
}

$@
