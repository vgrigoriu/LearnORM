LearnORM
========

What is the simplest way to use NHibernate's ConventionModelMapper with multiple RDMSs?

Can one use code-first EF with various RDMSs?

## Create user in Oracle DB

    -- USER SQL
    ALTER USER "LEARNORM"
    DEFAULT TABLESPACE "USERS"
    TEMPORARY TABLESPACE "TEMP"
    ACCOUNT UNLOCK ;

    -- QUOTAS
    ALTER USER "LEARNORM" QUOTA UNLIMITED ON USERS;

    -- ROLES

    -- SYSTEM PRIVILEGES
    GRANT CREATE SESSION TO "LEARNORM" ;
    GRANT CREATE TABLE TO "LEARNORM" ;
