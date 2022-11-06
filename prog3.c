#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <unistd.h>

int main(int argc, char *argv[], char *envp[])
{
	char name[]="file.fifo";
	
	(void)umask(0);
	mknod(name, 0666, 0);
	
	int fil = open(name, O_WRONLY);
	size_t sz = write(fil, argv[1], 14);
	close(fil);
	printf("FIFO file has been created\n");
	return 0;
}
