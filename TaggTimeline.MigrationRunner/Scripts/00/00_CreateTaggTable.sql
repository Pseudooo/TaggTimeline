
CREATE TABLE IF NOT EXISTS "Taggs" (
    "Id" UUID PRIMARY KEY,
    "Key" VARCHAR(255) NOT NULL,
    "CreatedDate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "ModifiedDate" TIMESTAMP WITH TIME ZONE NULL,
    "DeletedDate" TIMESTAMP WITH TIME ZONE NULL
);
