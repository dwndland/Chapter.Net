# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Added
- Added ForEach extension with the additional index parameter for each item.
### Supported .Net Versions
- .Net 8
- .Net 9

## [2.2.0] - 2024-12-01
### Added
- Added .net 9 to the supported .net versions.
### Supported .Net Versions
- .Net 8
- .Net 9

## [2.1.0] - 2024-09-20
### Added
- Added AddIfNeeded extensions to the List.
- Added AddIfNeeded extenbsion to the ICollection
### Fixed
- Fixed missing length check on the ObservableList.RemoveRange
### Supported .Net Versions
- .Net 8

## [2.0.0] - 2024-06-07
### Changed
- Update to support .Net 8 only.
### Supported .Net Versions
- .Net 8

## [1.1.0] - 2024-03-31
### Added
- Added more supported .net versions.
### Supported .Net Versions
- .Net Standard 2.0
- .Net Core 3.0
- .Net Framework 4.5
- .Net 5
- .Net 6
- .Net 7
- .Net 8

## [1.0.1] - 2024-02-14
### Fixed
- Added missing raise of PropertyChanged in the ValidatableObservableObject and NotifyDataErrorInfo to fully support refresh the UI after evaluate a property value.
### Supported .Net Versions
- .Net 6
- .Net 7
- .Net 8

## [1.0.0] - 2023-12-22
### Added
- Init project.
### Supported .Net Versions
- .Net 6
- .Net 7
- .Net 8
