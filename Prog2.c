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
    
    
    struct A
    {
        int key;
        char firstname[30];
        char surname[30];
        int birthyear;
    }
    *ptr, *inter_ptr;
    
    int fd = open("mapped.txt", O_RDWR, 0666);
    length = 40 * sizeof(struct A);
    
    
    if(fd == -1)
    {
    	printf("File open failed!\n");
    	exit(1);
    }
    
    
    ptr = (struct A*)mmap(NULL, length, PROT_WRITE | PROT_READ, MAP_SHARED, fd, 0) ;
    
    close(fd);
    
    inter_ptr = ptr;
    
    int index = 0;
    
    
    int choice = 0;
    
    while(choice != 4)
    {
    	printf("\n1 - Seek by key\n");
    	printf("2 - Seek by same values\n");
    	printf("3 - Seek by years of birth\n");
    	printf("4 - Exit\n");
    	printf("Your choice: ");
    	scanf("%u", &choice);
    	
    	if (choice == 1)
    	{
    		int key = -1;
    		int count = 0;
    		printf("Enter the key: ");
    		scanf("%u", &key);
    		for (int i = 0; i < 40; i++)
    		{
    			
    			if (inter_ptr[i].key == key)
    			{
    				printf("Key: %i Firstname: %s Surname: %s Birthyear: %u \n", inter_ptr[i].key, inter_ptr[i].firstname, inter_ptr[i].surname, inter_ptr[i].birthyear);
    				 count++;
    			}
    		}
    		if (count == 0)
    		{
    			printf("Key not found!\n");
    		}
    	}
    	
    	if (choice == 2)
    	{
    		int param = 0;
    		printf("1 - Firstname\n");
    		printf("2 - Surname\n");
    		printf("3 - Birthyear\n");
    		printf("Your choice: ");
    		scanf("%u", &param);
    		if(param == 1)
    		{
    			char firstname[30];
    			int count = 0;
    			printf("Entrer value: ");
    			scanf("%s", &firstname);
    			for(int i = 0; i < 40; i++)
    			{
    				if(!strcmp(inter_ptr[i].firstname, firstname))
    				{
    					printf("Key: %i Firstname: %s Surname: %s Birthyear: %u \n", inter_ptr[i].key, inter_ptr[i].firstname, inter_ptr[i].surname, inter_ptr[i].birthyear);
    					count ++;
    				}
    			}
    			
    			if (count == 0)
    			{
    				printf("Firstname not found!\n");
    			}
    			
    		}
    		
    		if(param == 2)
    		{
    			char surname[30];
    			int count = 0;
    			printf("Entrer value: ");
    			scanf("%s", &surname);
    			for(int i = 0; i < 40; i++)
    			{
    				if(!strcmp(inter_ptr[i].surname, surname))
    				{
    					printf("Key: %i Firstname: %s Surname: %s Birthyear: %u \n", inter_ptr[i].key, inter_ptr[i].firstname, inter_ptr[i].surname, inter_ptr[i].birthyear);
    					count ++;
    				}
    			}
    			
    			if (count == 0)
    			{
    				printf("Surname not found!\n");
    			}
    			
    		}
    		
    		if(param == 3)
    		{
    			int birthyear = 0;
    			int count = 0;
    			printf("Entrer value: ");
    			scanf("%u", &birthyear);
    			for(int i = 0; i < 40; i++)
    			{
    				if(inter_ptr[i].birthyear == birthyear)
    				{
    					printf("Key: %i Firstname: %s Surname: %s Birthyear: %u \n", inter_ptr[i].key, inter_ptr[i].firstname, inter_ptr[i].surname, inter_ptr[i].birthyear);
    					count ++;
    				}
    			}
    			
    			if (count == 0)
    			{
    				printf("Birthyear not found!\n");
    			}
    			
    		}
    	}
    	
    	if(choice == 3)
    	{
    		int birthyear_from;
    		int birthyear_to;
    		int count = 0;
    		printf("Enter interval\nFrom: ");
    		scanf("%u", &birthyear_from);
    		printf("To: ");
    		scanf("%u", &birthyear_to);
    		for(int i = 0; i < 40; i++)
		{
			if(inter_ptr[i].birthyear >= birthyear_from && inter_ptr[i].birthyear <= birthyear_to)
			{
				printf("Key: %i Firstname: %s Surname: %s Birthyear: %u \n", inter_ptr[i].key, inter_ptr[i].firstname, inter_ptr[i].surname, inter_ptr[i].birthyear);
				count ++;
			}
		}
		
		if (count == 0)
		{
			printf("Birthyear in interval not found!\n");
		}
    	}
    	
    }
    
    
    munmap((void*)ptr, length);
    return 0;
    
}
