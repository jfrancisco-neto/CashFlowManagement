rootFolder=$(dirname $0)
cmfProject=$rootFolder/src/CashFlowManagement

start_compose()
{
    docker compose -f $rootFolder/deployment/docker/compose/compose.yaml up
}

create_migration()
{
    local projectPath=$cmfProject/$1
    echo "Creating migration for $projectPath"

    dotnet ef migrations add $2 --project $projectPath
}

remove_migration()
{
    local projectPath=$cmfProject/$1
    echo "Removing migration for $projectPath"

    dotnet ef migrations remove --project $projectPath
}

apply_migration()
{
    local projectPath=$cmfProject/$1
    echo "Applying migrations $projectPath"

    dotnet ef database update --project $projectPath
}

$@
