#include <stdio.h>
#include <unistd.h>
#include <dirent.h>
#include <string.h>
#include <stdlib.h>
#include <sys/stat.h>

void deleteDir(const char path[]) {
	DIR *dir;
	struct stat ph, start;
	struct dirent *entry;
	char *file_path;
	stat(path, &ph);
	if (!(dir = opendir(path))) {
		printf("Directory is not exist!\n");
		exit(-1);
	}
	size_t length = strlen(path);
	while (entry = readdir(dir)) {
		if (!strcmp(entry->d_name, ".") || !strcmp(entry->d_name, "..")) {
			continue;
		}
		file_path = (char*)calloc(length + strlen(entry->d_name)+1,sizeof(char));
		strcpy(file_path, path);
		strcat(file_path, "/");
		strcat(file_path, entry->d_name);
		stat(file_path, &start);
		if (S_ISDIR(start.st_mode) != 0) {
			deleteDir(file_path);
			continue;
		}
		
	}
	if (unlink(file_path) == 0){
		printf("Deleted: %s\n", file_path);
		free(file_path);
	}
	if (rmdir(path) == 0) {
		printf("Deleted: %s\n", path);
	}
	closedir(dir);
}
int main(const int argc, char const *argv[]) {
	deleteDir(argv[1]);
	return 0;
}
