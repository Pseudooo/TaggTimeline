
-- Clear tables to add auth stuff without null constraints
DELETE FROM "Taggs";
DELETE FROM "Categories";
DELETE FROM "CategoryTagg";


-- Add user id to Taggs table
ALTER TABLE "Taggs"
    ADD COLUMN "UserId" text NOT NULL;

ALTER TABLE "Taggs"
    ADD CONSTRAINT "FK_Taggs_Users_UserId" 
        FOREIGN KEY ("UserId")
        REFERENCES "AspNetUsers" ("Id")
        ON DELETE CASCADE;


-- Add user id to categories table
ALTER TABLE "Categories"
    ADD COLUMN "UserId" text NOT NULL;

ALTER TABLE "Categories"
    ADD CONSTRAINT "FK_Categories_Users_UserId" 
        FOREIGN KEY ("UserId") 
        REFERENCES "AspNetUsers" ("Id")
        ON DELETE CASCADE;