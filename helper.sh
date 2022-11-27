rootFolder=$(dirname $0)
cmfProject=$rootFolder/src/CashFlowManagement

start_compose()
{
    docker compose -f $rootFolder/deployment/docker/compose/compose.yaml up
}

# account commands

create_migration()
{
    local projectPath=$cmfProject/$1
    echo "Creating migration for $projectPath"

    dotnet ef migrations add $2 --project $projectPath/$1.Persistence --startup-project $projectPath/$1.Api
}

remove_migration()
{
    local projectPath=$cmfProject/$1
    echo "Creating migration for $projectPath"

    dotnet ef migrations rm $2 --project $projectPath/$1.Persistence --startup-project $projectPath/$1.Api
}


$@
