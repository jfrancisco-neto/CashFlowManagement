rootFolder=$(dirname $0)
cmfProject=$rootFolder/src

start_compose()
{
    run_cmd "docker compose -f $rootFolder/deployment/docker/compose.yaml up"
}

stop_compose()
{
    run_cmd "docker compose -f $rootFolder/deployment/docker/compose.yaml stop"
}

load_envs()
{
    set -o allexport
    source .env
    set +o allexport
}

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
    load_envs
    run_cmd "dotnet ef migrations remove $2 --project $projectPath/$1.Persistence --startup-project $projectPath/$1.Api"
}

run()
{
    export ASPNETCORE_URLS="http://localhost:$3"
    load_envs
    run_cmd "dotnet run --project $cmfProject/$1/$1.$2"
}

run_all()
{
    run Account Api 5000
    run Transaction Api 5001
    run Balance Api 5002
}

build_all()
{
    run_cmd "dotnet build $cmfProject"
}

publish()
{
    if [ -z "$2" ]
    then
        run_cmd "dotnet publish $cmfProject/$1 -c Release -o $rootFolder/publish/$1"
    else
        run_cmd "dotnet publish $cmfProject/$1/$1.$2 -c Release -o $rootFolder/publish/$1.$2"
    fi
}

publish_all()
{
    publish Account Api
    publish Account Migrator
    publish Balance Api
    publish Balance Migrator
    publish Transaction Api
    publish Transaction Migrator
}

update_solution()
{
    run_cmd "dotnet sln $cmfProject add $cmfProject/*/*/*.csproj"
}

run_cmd()
{
    echo "CMD: $1"
    $1
}

help()
{
    echo "Available commands"
    echo ""
    echo "start_compose -> Start the docker compose for the infra (postgres, adminer)."
    echo "create_migration {project name} {migration name} -> Create migration of a specific project."
    echo "remove_migration {project name} -> Remove the last migration."
    echo "run {project name} {app} {port:optional} -> Run the app from specified project."
}

$@
