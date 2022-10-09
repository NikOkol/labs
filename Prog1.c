#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>
#include <sys/mman.h>
#include <fcntl.h>
#include <dirent.h>
#include <string.h>
#include <stdlib.h>

int main()
{
    size_t length;
    int fd = open("mapped.txt", O_RDWR | O_CREAT, 0666);
    
    struct A
    {
        int key;
        char firstname[30];
        char surname[30];
        int birthyear;
    }
    *ptr, *inter_ptr;
    
    length = 40 * sizeof(struct A);
    
    
    if(fd == -1)
    {
    	printf("File open failed!\n");
    	exit(1);
    }
    
    
    ftruncate(fd, length);
    ptr = (struct A*)mmap(NULL, length, PROT_WRITE | PROT_READ, MAP_SHARED, fd, 0) ;
    
    close(fd);
    
    inter_ptr = ptr;
    
    int index = -1;
    
    for (int i = 0; i < 40; i++)
    {
    	if (inter_ptr[i].key == 0)
    	{
    		index = i;
    		break;
    	}
    }
    
    int choice = 0;
    
    while(choice != 3)
    {
    	printf("\n1 - Add line\n");
    	printf("2 - Edit line\n");
    	printf("3 - Exit\n");
    	printf("Your choice: ");
    	scanf("%u", &choice);
    	
    	if (choice == 1)
    	{
    		
    		printf("Enter the key: ");
    		scanf("%u", &inter_ptr[index].key);
    		printf("Enter firstname: ");
    		scanf("%s", &inter_ptr[index].firstname);
    		printf("Enter surname: ");
    		scanf("%s", &inter_ptr[index].surname);
    		printf("Enter year of birth: ");
    		scanf("%u", &inter_ptr[index].birthyear);
    		index++;
    	}
    	
    	if (choice == 2)
    	{
    		int number = -1;
    		int key = -1;
    		printf("Enter the key of the editing line: ");
    		scanf("%u", &key);
    		for (int i = 0; i < 40; i++)
    		{
    			if (inter_ptr[i].key == key)
    			{
    				number = i;
    				break;
    			}
    		}
    		printf("Enter the key: ");
    		scanf("%u", &inter_ptr[number].key);
    		printf("Enter firstname: ");
    		scanf("%s", &inter_ptr[number].firstname);
    		printf("Enter surname: ");
    		scanf("%s", &inter_ptr[number].surname);
    		printf("Enter year of birth: ");
    		scanf("%u", &inter_ptr[number].birthyear);
    	}
    }
    
    
    munmap((void*)ptr, length);
    return 0;
    
}
