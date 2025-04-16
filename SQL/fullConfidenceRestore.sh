PGDATABASE='postgres'
PGUSER='postgres'
PGPASSWORD='password'
PGHOST='127.0.0.1'
            
kubectl port-forward svc/zas-db-svc 5432:5432 -n zasanddas &
while ! nc -z localhost 5432; do
    sleep 0.5
done

psql postgres < zas.sql