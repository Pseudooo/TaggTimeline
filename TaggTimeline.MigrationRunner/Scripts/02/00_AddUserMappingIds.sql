
CREATE TABLE IF NOT EXISTS "UserMappings" (
    "Id" uuid NOT NULL,
    "UserId" text NOT NULL,
    CONSTRAINT "PK_UserMappings" PRIMARY KEY ("Id"),
    UNIQUE ("UserId")
);

-- Create a dummy user mapping to link existing user specific content too
INSERT INTO "UserMappings" ("Id", "UserId")
    VALUES ('00000000-0000-0000-0000-000000000000'::uuid, '0_')
    ON CONFLICT DO NOTHING;

ALTER TABLE "Taggs"
    ADD COLUMN "UserMappingId" uuid NOT NULL
    DEFAULT '00000000-0000-0000-0000-000000000000'::uuid;

ALTER TABLE "Taggs" 
    ALTER COLUMN "UserMappingId"
    DROP DEFAULT;

ALTER TABLE "Taggs"
    ADD CONSTRAINT "FK_Taggs_UserMappings_UserMappingId" FOREIGN KEY ("UserMappingId") REFERENCES "UserMappings";

ALTER TABLE "Categories"
    ADD COLUMN "UserMappingId" uuid NOT NULL
    DEFAULT '00000000-0000-0000-0000-000000000000'::uuid;

ALTER TABLE "Categories" 
    ALTER COLUMN "UserMappingId"
    DROP DEFAULT;

ALTER TABLE "Categories"
    ADD CONSTRAINT "FK_Categories_UserMappings_UserMappingId" FOREIGN KEY ("UserMappingId") REFERENCES "UserMappings";
