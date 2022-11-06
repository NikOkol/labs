#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <unistd.h>

int main(void)
{
	char str[20];
	char name[]="file.fifo";
	
	(void)umask(0);
	
	int file = open(name, O_RDONLY);
	size_t size = read(file, str, 20);
	
	printf("%s\n", str);
	close(file);
	
	return 0;
}
