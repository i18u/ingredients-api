CREATE KEYSPACE IF NOT EXISTS cookbook WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication' : 3 };

CREATE TABLE IF NOT EXISTS ingredient (
    id UUID,
    name varchar,
    description varchar,
    tags set<varchar>,
    PRIMARY KEY (id)
);