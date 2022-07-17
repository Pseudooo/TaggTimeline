
CREATE TABLE IF NOT EXISTS "Instances" (
    "Id" uuid NOT NULL,
    "TaggId" uuid NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Instances" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Instances_Taggs_TaggId" FOREIGN KEY ("TaggId") REFERENCES "Taggs" ("Id")
);
