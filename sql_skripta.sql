-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema 17037_DB
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema 17037_DB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `17037_DB` DEFAULT CHARACTER SET utf8 ;
USE `17037_DB` ;

-- -----------------------------------------------------
-- Table `17037_DB`.`bus`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`bus` (
  `bus_id` INT NOT NULL AUTO_INCREMENT,
  `duzina` INT NULL DEFAULT NULL,
  `sirina` INT NULL DEFAULT NULL,
  `broj_sjedala` INT NULL DEFAULT NULL,
  `max_brzina` INT NULL DEFAULT NULL,
  PRIMARY KEY (`bus_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`korisnik`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`korisnik` (
  `korisnik_id` INT NOT NULL AUTO_INCREMENT,
  `ime` VARCHAR(45) NULL DEFAULT NULL,
  `prezime` VARCHAR(45) NULL DEFAULT NULL,
  `datum_rodenja` DATE NULL DEFAULT NULL,
  `oib` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`korisnik_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`vozac`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`vozac` (
  `vozac_id` INT NOT NULL AUTO_INCREMENT,
  `korisnik` INT NOT NULL,
  PRIMARY KEY (`vozac_id`),
  INDEX `fk_vozac_korisnik1_idx` (`korisnik` ASC),
  CONSTRAINT `fk_vozac_korisnik1`
    FOREIGN KEY (`korisnik`)
    REFERENCES `17037_DB`.`korisnik` (`korisnik_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`grad`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`grad` (
  `grad_id` INT NOT NULL AUTO_INCREMENT,
  `naziv` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`grad_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`stanica`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`stanica` (
  `stanica_id` INT NOT NULL AUTO_INCREMENT,
  `naziv_stanice` VARCHAR(45) NOT NULL,
  `grad` INT NOT NULL,
  PRIMARY KEY (`stanica_id`),
  INDEX `fk_stanica_grad1_idx` (`grad` ASC),
  CONSTRAINT `fk_stanica_grad1`
    FOREIGN KEY (`grad`)
    REFERENCES `17037_DB`.`grad` (`grad_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`linija`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`linija` (
  `linija_id` INT NOT NULL AUTO_INCREMENT,
  `naziv_linije` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`linija_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`medustanice`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`medustanice` (
  `start` TINYINT(1) NOT NULL,
  `end` TINYINT(1) NOT NULL,
  `linija` INT NOT NULL,
  `stanica` INT NOT NULL,
  PRIMARY KEY (`linija`, `stanica`),
  INDEX `fk_medustanice_linija1_idx` (`linija` ASC),
  INDEX `fk_medustanice_stanica1_idx` (`stanica` ASC),
  CONSTRAINT `fk_medustanice_linija1`
    FOREIGN KEY (`linija`)
    REFERENCES `17037_DB`.`linija` (`linija_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_medustanice_stanica1`
    FOREIGN KEY (`stanica`)
    REFERENCES `17037_DB`.`stanica` (`stanica_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`voznja`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`voznja` (
  `voznja_id` INT NOT NULL AUTO_INCREMENT,
  `datum_voznje` DATE NULL DEFAULT NULL,
  `vozac` INT NOT NULL,
  `bus` INT NOT NULL,
  `linija` INT NOT NULL,
  PRIMARY KEY (`voznja_id`),
  INDEX `fk_voznja_vozac1_idx` (`vozac` ASC),
  INDEX `fk_voznja_bus1_idx` (`bus` ASC),
  INDEX `fk_voznja_linija1_idx` (`linija` ASC),
  CONSTRAINT `fk_voznja_vozac1`
    FOREIGN KEY (`vozac`)
    REFERENCES `17037_DB`.`vozac` (`vozac_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_voznja_bus1`
    FOREIGN KEY (`bus`)
    REFERENCES `17037_DB`.`bus` (`bus_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_voznja_linija1`
    FOREIGN KEY (`linija`)
    REFERENCES `17037_DB`.`linija` (`linija_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`popust`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`popust` (
  `popust_id` INT NOT NULL AUTO_INCREMENT,
  `naziv_popusta` VARCHAR(45) NOT NULL,
  `kolicina_popusta` INT NOT NULL,
  PRIMARY KEY (`popust_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `17037_DB`.`karta`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `17037_DB`.`karta` (
  `karta_id` INT NOT NULL AUTO_INCREMENT,
  `popust` INT NOT NULL,
  `vozac` INT NOT NULL,
  `voznja` INT NOT NULL,
  PRIMARY KEY (`karta_id`),
  INDEX `fk_karta_popust1_idx` (`popust` ASC),
  INDEX `fk_karta_vozac1_idx` (`vozac` ASC),
  INDEX `fk_karta_voznja1_idx` (`voznja` ASC),
  CONSTRAINT `fk_karta_popust1`
    FOREIGN KEY (`popust`)
    REFERENCES `17037_DB`.`popust` (`popust_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_karta_vozac1`
    FOREIGN KEY (`vozac`)
    REFERENCES `17037_DB`.`vozac` (`vozac_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_karta_voznja1`
    FOREIGN KEY (`voznja`)
    REFERENCES `17037_DB`.`voznja` (`voznja_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
